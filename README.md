# Object-Oriented Analysis of MIDI Files

Bu proje, MIDI dosyalarının nesne tabanlı bir şekilde işlenmesi ve analiz edilmesini amaçlamaktadır. Proje, MIDI dosyalarındaki verilerin işlenmesini ve analizini daha kolay ve verimli hale getirebilmek için nesne yönelimli programlama (OOP) yaklaşımını kullanır.

## Özellikler
- MIDI dosyalarını okuma ve yazma.
- MIDI verilerinin nesne tabanlı analizi.
- MIDI dosyalarının içindeki enstrüman, tempo, ritim gibi bilgilerin ayrıştırılması.
- Kullanıcı dostu ve genişletilebilir yapılar.

## Gereksinimler
- Python 3.x veya daha yeni bir sürüm.
- Gerekli kütüphaneler:
  - `mido` (MIDI dosyalarını işlemek için)
  - Diğer gereksinimler proje içinde belirtilmiştir.


## Kurulum

Proje dosyalarını GitHub'dan klonlayarak veya indirerek başlayabilirsiniz:
git clone https://github.com/yourusername/object-oriented-analysis-of-midi-files.git

## Kullanım

Projenin temel işlevini kullanmak için şu adımları izleyebilirsiniz:

from midi_analyzer import MidiAnalyzer

# MIDI dosyasını yükle
midi_file = MidiAnalyzer('path_to_your_midi_file.mid')

# Dosyayı analiz et
midi_file.analyze()

# Analiz sonuçlarını yazdır
midi_file.print_analysis()




