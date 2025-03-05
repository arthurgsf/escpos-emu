
using System.Text;
using Printer.Emulator;

// var listener = new TcpListener(IPAddress.Loopback, 5000);
// listener.Start();
//
// using TcpClient client = await listener.AcceptTcpClientAsync();
//
// await using NetworkStream stream = client.GetStream();
//
// var receiveBuffer = GC.AllocateArray<byte>(1024, true);
// var bufferMemory = receiveBuffer.AsMemory();
// var nBytes = await stream.ReadAsync(bufferMemory);
// var data = bufferMemory[..nBytes];

var printer = new ReceiptPrinter(PaperConfiguration.Default);
// printer.FeedEscPos(Encoding.ASCII.GetString(data.Span));

var data = File.ReadAllText("<input _ file>", Encoding.ASCII);
printer.FeedEscPos(data);

var encoded = printer.ReceiptStack.First().Render();

// Save image to a file
string filePath = "<output _ file>";
using var fileStream = File.OpenWrite(filePath);
encoded.SaveTo(fileStream);


