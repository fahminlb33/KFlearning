# KFlearning Suite

KFlearning adalah platform e-learning untuk belajar algoritma dan pemrograman khususnya bagi mahasiswa 
jurusan ilmu komputer.

|       Mirror            |                            Link Download                          |
|-------------------------|:-----------------------------------------------------------------:|
| KFlearning stable       | [KFlearning Home](https://kflearning.kodesiana.com)               |
| KFlearning old/CI build | [Releases](https://github.com/fahminlb33/KFlearning/releases)     |
| E-modul praktikum       | [KFlearning E-Modul](https://kflearning.kodesiana.com/modul.html) |

Repositori ini berisi source code untuk aplikasi KFlearning dan KFmaintenance yang merupakan standar
aplikasi untuk menunjang kegiatan praktikum di laboratorium komputer Universitas Pakuan.

| Build | Status |
|-------|--------|
|Canary |![Canary](https://github.com/fahminlb33/KFlearning/workflows/Canary%20Build/badge.svg) |
|Stable |![Stable](https://github.com/fahminlb33/KFlearning/workflows/Stable%20Build/badge.svg) |

## KFlearning

KFlearning merupakan aplikasi *launcher* untuk membantu membuat dan mengorganisasi proyek program
yang Anda kerjakan. Program ini ditujukan untuk membiasakan mahasiswa untuk menggunakan aplikasi
Visual Studio Code.

- Berisi 4 template siap pakai, yaitu: Web PHP, Console (C++), GUI (C++ Freeglut), dan Data Science (Python).
- Otomatis membuat konfigurasi `.vscode` untuk menjalankan aplikasi (integrasi dengan Visual Studio Code).
- Riwayat proyek yang dikerjakan.

## KFmaintenance

KFmaintenance merupakan aplikasi untuk membantu asisten praktikum melakukan *maintenance* dan mengatur
hak akses mahasiswa pada komputer di laboratorium komputer. Aplikasi ini dapat bekerja dalam jaringan.

- Penguncian wallpaper, Task Manager, dan fitur administratif Windows lainnya.
- Remote shutdown pada jaringan.
- Berbagi file menggunakan Torrent pada jaringan (integrasi dengan qBittorrent).
- CLIS Filler (untuk input data ke CLIS UNPAK).

Untuk menggunakan CLIS Filler, Anda harus mengunduh [Chrome Driver](https://chromedriver.chromium.org/downloads).
Sesuaikan versi Chrome drier dengan versi Chrome yang Anda gunakan.

## Kebutuhan Sistem

- Windows 7 SP1
- .NET Framework 4.5
- Visual Studio User Installer (KFlearning)
- qBittorrent (KFmaintenance)

## Bantuan dan Kontribusi

Jika Anda memiliki pertanyaan, silakan membuat Issue pada repositori ini atau menghubungi asisten praktikum atau
developer aplikasi ini melalui WhatsApp dan kontak profil GitHub.

Kontribusi Anda akan sangat membantu dalam pengembangan aplikasi ini. Jangan malu-malu untuk membuat pull request
ya! :smile:
