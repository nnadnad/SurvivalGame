# IF3210-2020-Unity-13517128

### Deskripsi aplikasi
Sebuah game wave survival dengan menggunakan unity3D dibangun diatas platform 2D. Game ini mengharuskan kita untuk menembak musuh dan mendapatkan score, Kondisi game over akan didapat ketika nyawa player menjadi 0

### Cara kerja, terutama mengenai pemenuhan spesifikasi aplikasi
* Cara kerja game, terdapat main menu, dimana user dapat memeilih untuk start game atau quit
* Player dapat bergerak menggunakan keyboard, A,D dan space. `A` dan `D` untuk berjalan dan `SPACE` untuk jump.
* Player dapat menembak musuh dengan menggukan mouse.
* Pergerakan camera mengikuti player
* Musuh digenerta random dengan path finding, dan lokasi wave spawner nya sudah ditentukan
* Kondisi game berakhir ketika darah pemain menjadi 0

### Library yang digunakan dan justifikasi penggunaannya
* Library path finding : untuk melakukan A* path finding antara musuh dan player
* Library Scene Component : untuk membuat berbagai menu seperti main menu, game over, dan game menu

### Screenshot aplikasi
