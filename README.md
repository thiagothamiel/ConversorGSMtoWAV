# ConversorGSMtoWAV
Conversão de arquivos GSM para WAV
- Formato muito utilizado no mercado SIP/VOIP
- Crei o seu portal de download de gravações GSM, dsiponibilizando o download já em formato WAV.

#Autor:ThiagoThamiel
#Site: www.thiagothamiel.com

#Instalação:
Por padrão na primeira instalação, realize a instalação do SetupConversorGSMtoWAV.mis
- Extrai/Instale a aplicação da SOX (sox-14.4.1-win32.exe) na pasta "C:\GSMtoWAV\SOX".
- Caso a Pasta "C:\GSMtoWAV\GRAVACOES" não seja criada automaticamente, crie manualmente.
- Reinicie o serviço do windows (services.msc):  "ConversorGSMtoWAV"
- Copie o arquivo a ser convertido na pasta "C:\GSMtoWAV\GRAVACOES" o mesmo será convertido e disponibilizado em wav.

#GSMtoWAV (Estrutura de pasta padrão)
- C:\GSMtoWAV\GRAVACOES
- C:\GSMtoWAV\GRAVACOES
- C:\GSMtoWAV\SOX
