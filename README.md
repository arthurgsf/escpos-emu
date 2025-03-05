﻿# ESC/POS Receipt Printer Emulator
🖨️ **This app emulates a networked receipt printer to test your ESC/POS commands against.**

![Emulator](https://user-images.githubusercontent.com/6772638/160160456-faf28c07-72ac-43c4-907f-6258376cd483.png)

### About
- Forked from (Originally a Windows application (WPF + .NET 6))
- Changed rendering lib to SkiaSharp
- Did some minor refactoring 
- Binds to a TCP/IP interface and listens for ESC/POS commands

⚠️Edit Program.cs and change input/output files, or bind input to a tcp endpoint.

### Supported commands

⚠️ Support is currently limited to only a subset of ESC/POS. Even the commands listed here may only be partially implemented.

- Raw Text
- FF: Form feed
- HT: Horizontal tabs
- LF: Line feed
- CR: Carriage return
- ESC Commands:
  - Initialize printer (`ESC @`)
  - Toggle italic (`ESC 4` / `ESC 5`)
  - Select font (`ESC M`)
  - Select justification (`ESC a`)
  - Select line spacing (`ESC 2` / `ESC 3`)
  - Toggle emphasis (`ESC E`)
  - Toggle underline (`ESC -`)
  - Set print text mode (`ESC !`)
  - Paper full cut (`ESC m`)
  - Paper partial cut (`ESC i`)
  - Paper feed N lines (`ESC d`)
  - Paper feed (`ESC J`)
- FS Commands:
  - Print stored logo (`FS p`)
  - Paper auto cut (`FS }`)
- GS Commands:
  - Select character size 
  - Select cut mode and cut paper
  - Paper eject (`GS e`)

### Emulated printer

This program emulates a printer with the following specifications:

 - 80mm paper width
 - 76mm printing width
 - 203x203dpi
 - ASCII Font A: 12x24 pixels
 - ASCII Font B: 9x24 pixels
 - ASCII Font C: 24x48 pixels
 - ASCII Font D: 16x24 pixels
 - Automatic line feed

### References

https://aures-support.com/DATA/drivers/Imprimantes/Commande%20ESCPOS.pdf<br>
https://www.starmicronics.com/support/Mannualfolder/escpos_cm_en.pdf<br>
https://www.printercontroller.com/kcfinder/upload/files/esc_pos_commands_1_0_7.pdf<br>
https://www.electronicaembajadores.com/Datos/pdf1/i5/i5tm/i5tm004.pdf<br>
https://download4.epson.biz/sec_pubs/pos/reference_en/escpos/<br>
https://escpos.readthedocs.io/en/latest/commands.html<br>
https://tabshop.smartlab.at/help-topics/help-esc-pos-codes.html<br>
