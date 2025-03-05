﻿using Printer.Emulator;

namespace Printer.EscPos.Commands.ESC;

public class InitializePrinterCommand : BaseCommandNoArgs
{
    public override string Prefix => EscPosInterpreter.ESC + "@";
    
    public override void Execute(ReceiptPrinter printer, string? args)
    {
        printer.Initialize();
    }
}