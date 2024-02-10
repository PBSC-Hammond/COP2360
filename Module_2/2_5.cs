// string, int and bool types are examples of predefined types:

string message = "Hello world";
string upperMessage = message.ToUpper();
Console.WriteLine (upperMessage);               // HELLO WORLD

int x = 2015;
message = message + x.ToString();
Console.WriteLine (message);                    // Hello world2015

bool simpleVar = false;
if (simpleVar)
  Console.WriteLine ("This will not print");

int y = 5000;
bool lessThanAMile = y < 5280;
if (lessThanAMile)
  Console.WriteLine ("This will print");


