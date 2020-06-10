#include "winsock2.h"
#include "windows.h"
#include "mongoose.h"

#define BUFSIZE 4096

static const char* s_http_port = "21002";
static struct mg_serve_http_opts s_http_server_opts;

static void ev_handler(struct mg_connection* nc, int ev, void* p)
{
    if (ev != MG_EV_HTTP_REQUEST) return;
    mg_serve_http(nc, (struct http_message*)p, s_http_server_opts);
}

int main(int argc, char* argv[])
{
    // check if path is supplied
    if (argc != 2)
    {
        printf("Serve path not specified.");
        return -1;
    }

    // check if path exists
    DWORD dwAttrib = GetFileAttributesA(argv[1]);
    if (dwAttrib == INVALID_FILE_ATTRIBUTES || !(dwAttrib & FILE_ATTRIBUTE_DIRECTORY))
    {
        printf("Serve path not exists.");
        return -2;
    }

    // initiate server
    struct mg_mgr mgr;
    struct mg_connection* nc;

    mg_mgr_init(&mgr, NULL);
    nc = mg_bind(&mgr, s_http_port, ev_handler);
    if (nc == NULL)
    {
        printf("Failed to create listener\n");
        return -3;
    }

    mg_set_protocol_http_websocket(nc);
    s_http_server_opts.document_root = argv[1];
    s_http_server_opts.enable_directory_listing = "yes";

    // get full path to source
    TCHAR buffer[BUFSIZE] = TEXT("");
    GetFullPathName(argv[1], BUFSIZE, buffer, NULL);

    printf("Starting web server on port %s\n", s_http_port);
    printf("Serving files from %s\n", buffer);

    // start message loop
    for (;;)
    {
        mg_mgr_poll(&mgr, 1000);
    }

    printf("Freeing memory...");
    mg_mgr_free(&mgr);

    return 0;
}
