# ESC/POS Receipt Printer Emulator v1.01
🖨️ **This app emulates a networked receipt printer to test your ESC/POS commands against.**

![Emulator](https://user-images.githubusercontent.com/6772638/160160456-faf28c07-72ac-43c4-907f-6258376cd483.png)

### About
- Windows application (WPF + .NET 6)
- Binds to a TCP/IP interface and listens for ESC/POS commands
- Logs commands and visually represents the resulting receipt(s)
- With italics text cutting bug corrected

👷 **This is a working in progress.** Use at your own risk and keep your expectations low. :)

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
