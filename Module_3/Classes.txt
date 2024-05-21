Fields

// A field is a variable that is a member of a class or struct.

var o = new Octopus();
o.Age.Dump();

class Octopus
{
  string name;
  public int Age = 10;
  static readonly int legs = 8, eyes = 1;
}
Fields - readonly

// Readonly fields let you create *immutable* classes.

var o = new Octopus ("Jack");
o.Name.Dump();  
o.Legs = 20;  // Compile-time error

class Octopus
{
  public readonly string Name;
  public readonly int Legs = 8;
  
  public Octopus (string name)
  {
    Name = name;
  }
}
Constants - scoped to class

// Constants are factored out at compile-time and baked into the calling site.

Test.Message.Dump();

public class Test
{
  public const string Message = "Hello World";
}
Constants - scoped to method

// Here, the calculation is performed at compile-time:

const double twoPI  = 2 * System.Math.PI;
twoPI.Dump();
Methods - Expression-bodied

// Foo1 and Foo2 are equivalent:

Foo1 (10).Dump();
Foo2 (10).Dump();

int Foo1 (int x) { return x * 2; }
int Foo2 (int x) => x * 2;
Local methods

void Main()
{
  WriteCubes();
}

void WriteCubes()
{
  Console.WriteLine (Cube (3));
  Console.WriteLine (Cube (4));
  Console.WriteLine (Cube (5));

  int Cube (int value) => value * value * value;
}
Local methods - with top-level statements

// When you use top-level statements, your methods are implicitly local.
// We can verify this by trying to access a variable outside the method:

int x = 3;
Foo();

// Foo is a local method
void Foo() => Console.WriteLine (x);    // We can access x
Methods - Overloading

// We can overload Foo as follows:

void Foo (int x)          { "int".Dump(); }
void Foo (double x)       { "double".Dump(); }
void Foo (int x, float y) { "int, float".Dump(); }
void Foo (float x, int y) { "float, int".Dump(); }

// We have to use a Main() method here to test this,
// because methods in top-level statements are *local methods*
// which cannot be overloaded.

void Main()
{
  Foo (123);      // int
  Foo (123.0);    // double
  Foo (123, 123F);  // int, float
  Foo (123F, 123);  // float, int
}
Methods - Illegal Overloading

// The following overloads are prohibited:

void  Foo (int x);
float Foo (int x);           // Compile-time error

void  Goo (int[] x);
void  Goo (params int[] x);  // Compile-time error

void Hoo (int x);
void Hoo (ref int x);      // OK so far
void Hoo (out int x);      // Compile-time error

void Main() {}
Constructors

Panda p = new Panda ("Petey");   // Call constructor

public class Panda
{
  string name;                   // Define field
  public Panda (string n)        // Define constructor
  {
    name = n;                    // Initialization code (set up field)
  }
}
Constructors - Overloading

// You can also overload constructors.
// Note the use of the "this" keyword to call another constructor:

new Wine (78).Dump();
new Wine (78, 2001).Dump();

public class Wine
{
  public decimal Price;
  public int Year;
  public Wine (decimal price) => Price = price;
  public Wine (decimal price, int year) : this (price) => Year = year;
}
Constructors - Nonpublic

// A common reason to have a nonpublic constructor is to control instance creation via a
// static method call:

Class1 c1 = Class1.Create();  // OK
Class1 c2 = new Class1();    // Error: Will not compile

public class Class1
{
  Class1() { }    // Private constructor
  
  public static Class1 Create()
  {
    // Perform custom logic here to create & configure an instance of Class1
    /* ... */
    return new Class1();
  }
}
Deconstructors

// To call the deconstructor, we use the following special syntax:
var rect = new Rectangle (3, 4);
(float width, float height) = rect;          // Deconstruction
Console.WriteLine (width + " " + height);    // 3 4

// We can also use implicit typing:  
var (x, y) = rect;          // Deconstruction
Console.WriteLine (x + " " + y);

// If the variables already exist, we can do a *deconstructing assignment*:
(x, y) = rect; 
Console.WriteLine (x + " " + y);

// The following mix-and-match is permitted, from C# 10:
double x1 = 0;
(x1, double y2) = rect;

class Rectangle
{
  public readonly float Width, Height;

  public Rectangle (float width, float height)
  {
    Width = width;
    Height = height;
  }

  public void Deconstruct (out float width, out float height)
  {
    width = Width;
    height = Height;
  }
}
Object Initializers

// Fields or properties can be initialized in a single statement directly after construction:

// Object initialization syntax. Note that we can still specify constructor arguments:
  
  Bunny b1 = new Bunny { Name="Bo", LikesCarrots=true, LikesHumans=false };
  Bunny b2 = new Bunny ("Bo")     { LikesCarrots=true, LikesHumans=false };
  
  b1.Dump(); b2.Dump();

public class Bunny
{
  public string Name;
  public bool LikesCarrots;
  public bool LikesHumans;
  
  public Bunny () {}
  public Bunny (string n) { Name = n; }
}
Object Initializer Alternative - Optional Parameters

// Instead of using object initializers, we could make Bunny’s constructor accept optional parameters.
// This has both pros and cons (see book):

Bunny b = new Bunny (
    name: "Bo",
    likesCarrots: true);
  
  b.Dump();

public class Bunny
{
  public string Name;
  public bool LikesCarrots;
  public bool LikesHumans;

  public Bunny (
    string name,
    bool likesCarrots = false,
    bool likesHumans = false)
  {
    Name = name;
    LikesCarrots = likesCarrots;
    LikesHumans = likesHumans; 
  }
}
The this Reference

// The this reference refers to the instance itself:

new Panda().Marry (new Panda());

public class Panda
{
  public Panda Mate;

  public void Marry (Panda partner)
  {
    Mate = partner;
    partner.Mate = this;
  }
}
Properties

// Properties look like fields from the outside but internally, they contain logic, like methods:

var stock = new Stock();
stock.CurrentPrice = 123.45M;
stock.CurrentPrice.Dump();
  
var stock2 = new Stock { CurrentPrice = 83.12M };
stock2.CurrentPrice.Dump();

public class Stock
{
  decimal currentPrice;           // The private "backing" field
  
  public decimal CurrentPrice     // The public property
  {
    get { return currentPrice; } set { currentPrice = value; }
  }
}
Properties - calculated & read-only

// The Worth Property is a read-only calculated property.

var stock = new Stock { CurrentPrice = 50, SharesOwned = 100 };
stock.Worth.Dump();

public class Stock
{
  decimal currentPrice;           // The private "backing" field
  public decimal CurrentPrice     // The public property
  {
    get { return currentPrice; } set { currentPrice = value; }
  }

  decimal sharesOwned;           // The private "backing" field
  public decimal SharesOwned     // The public property
  {
    get { return sharesOwned; } set { sharesOwned = value; }
  }

  public decimal Worth
  {
    get { return currentPrice * sharesOwned; }
  }
}
Properties - expression-bodied

// The Worth Property is now an expression-bodied property.

var stock = new Stock { CurrentPrice = 50, SharesOwned = 100 };
stock.Worth.Dump();

public class Stock
{
  decimal currentPrice;           // The private "backing" field
  public decimal CurrentPrice     // The public property
  {
    get { return currentPrice; } set { currentPrice = value; }
  }

  decimal sharesOwned;           // The private "backing" field
  public decimal SharesOwned     // The public property
  {
    get { return sharesOwned; } set { sharesOwned = value; }
  }

  public decimal Worth => currentPrice * sharesOwned;    // Expression-bodied property

  // From C# 7, we can take this further, and write both the get and set accessors in
  // expression-bodied syntax:
  public decimal Worth2
  {
    get => currentPrice * sharesOwned;
    set => sharesOwned = value / currentPrice;
  }

}
Automatic Properties

// Here's the preceding example rewritten with two automatic properties:

var stock = new Stock { CurrentPrice = 50, SharesOwned = 100 };
stock.Worth.Dump();

public class Stock
{
  public decimal CurrentPrice { get; set; }   // Automatic property
  public decimal SharesOwned { get; set; }    // Automatic property

  public decimal Worth
  {
    get { return CurrentPrice * SharesOwned; }
  }
}
Property Initializers

var stock = new Stock();
stock.CurrentPrice.Dump();
stock.Maximum.Dump();

public class Stock
{
  public decimal CurrentPrice { get; set; } = 123;
  public int Maximum { get; } = 999;
}
Properties - get & set accessibility

// In this example, the set accessors are private while the get accessors are public:

new Foo { X = 5 };    // Will not compile - X has a private set accessor.

public class Foo
{
  private decimal x;
  public decimal X
  {
    get         { return x;  }
    private set { x = Math.Round (value, 2); }
  }
  
  public int Auto { get; private set; }  // Automatic property
}
Indexers

// You can implement custom indexers with the this keyword:

Sentence s = new Sentence();
Console.WriteLine (s[3]);       // fox
s[3] = "kangaroo";
Console.WriteLine (s[3]);       // kangaroo

// Test the indexers that use C#'s Indices and Ranges:

Console.WriteLine (s [^1]);                // fox  
string[] firstTwoWords = s [..2].Dump();   // (The, quick)

class Sentence
{
  string[] words = "The quick brown fox".Split();
  
  public string this [int wordNum]      // indexer
  { 
    get { return words [wordNum];  }
    set { words [wordNum] = value; }
  }

  // In C# 8, we can also define indexers that use the Index & Range types:
  public string this [Index index] => words [index];
  public string[] this [Range range] => words [range];

}
Primary Constructors

// From C# 12, you can include a parameter list directly after a class (or struct) declaration.
// This instructs the compiler to automatically build a primary constructor using the primary
// constructor parameters (firstName and lastName).

Person p = new Person ("Alice", "Jones");
p.Print();    // Alice Jones

class Person (string firstName, string lastName)
{
  public void Print() => Console.WriteLine (firstName + " " + lastName);
}

// See also query://../Inheritance/Inheritance_with_primary_constructors
Primary Constructors - additional constructors

Person p = new Person ("Alice", "Jones", 33);
p.Print();    // Alice Jones

class Person (string firstName, string lastName)
{
  int _age;
  
  // The constructor that C# builds is called primary because any additional constructors
  // that you choose to (explicitly) write must invoke it:
  
  public Person (string firstName, string lastName, int age)
    : this (firstName, lastName)    // Must call the primary constructor
  {
    _age = age;
  }
    
  public void Print() => Console.WriteLine (firstName + " " + lastName + " " + _age);
}

// See also query://../Inheritance/Inheritance_with_primary_constructors
Primary Constructors and initializers

// The accessibility of primary constructor parameters extends to field and property initializers.
// In the following example, we use field and property initializers to assign firstName to a public
// field, and lastName to a public property:

Person p = new Person ("Alice", "Jones");
p.Dump();

class Person (string firstName, string lastName)
{
  public readonly string FirstName = firstName;  // Field
  public string LastName { get; } = lastName;    // Property
}
Primary Constructors - masking

// Fields (or properties) can reuse primary constructor parameter names: 

Person p = new Person ("Alice", "Jones");
p.Print();

class Person (string firstName, string lastName)
{
  // In this scenario, the field or property takes precedence, thereby masking
  // the primary constructor parameter, except on the righthand side of field and
  // property initializers:  
  readonly string firstName = firstName;
  readonly string lastName = lastName;

  public void Print() => Console.WriteLine (firstName + " " + lastName);
}
Primary Constructors - validating

new Person1 ("Alice", "Jones").Print();   // Alice Jones
new Person2 ("Alice", "Jones").Print();   // Alice JONES
var p3 = new Person3 ("Alice", null);     // throws ArgumentNullException

class Person1 (string firstName, string lastName)
{
  // Sometimes it’s useful to perform computation in field initializers: 
  public readonly string FullName = firstName + " " + lastName;

  public void Print() => Console.WriteLine (FullName);
}

class Person2 (string firstName, string lastName)
{
  // Save an uppercase version of lastName to a field of the same name
  // (masking the original value):
  readonly string lastName = lastName.ToUpper();
  
  public void Print() => Console.WriteLine (firstName + " " + lastName);
}

class Person3 (string firstName, string lastName)
{
  // Validate lastName upon construction, ensuring that it cannot be null:
  readonly string lastName = (lastName == null)
    ? throw new ArgumentNullException ("lastName")
    : lastName;
    
  public void Print() => Console.WriteLine (firstName + " " + lastName);
}
Static Constructors

// A static constructor executes once per type, rather than once per instance:

// Type is initialized only once
new Test();
new Test();
new Test();

class Test
{
  static Test()
  {
    Console.WriteLine ("Type Initialized");
  }
}
Static Constructors & Field Initialization Order

// Static field initializers run just before the static constructor is called:

Foo.X.Dump ("X");  // 0
Foo.Y.Dump ("Y");  // 3

class Foo
{
  public static int X = Y;    // 0
  public static int Y = 3;    // 3
}
Static Constructors & Field Initialization Order (Constructor Call)

// Another way to go awry:

Console.WriteLine (Foo.X);   // 3

Util.NewProcess = true;      // Force LINQPad to create new process on each run.

class Foo
{
  public static Foo Instance = new Foo();
  public static int X = 3;
  
  Foo() => Console.WriteLine (X);   // 0
}
Partial Types

// Partial types allow a type definition to be split—typically across multiple files:

new PaymentForm { X = 3, Y = 4 }.Dump();

partial class PaymentForm { public int X; }
partial class PaymentForm { public int Y; }
Partial Methods

// A partial type may contain partial methods. These let an auto-generated partial type
// provide customizable hooks for manual authoring.

var paymentForm = new PaymentForm (50);

partial class PaymentForm    // In auto-generated file
{  
  public PaymentForm (decimal amount)
  {
    ValidatePayment (amount);
    // ...
  }

  partial void ValidatePayment (decimal amount);
}

partial class PaymentForm    // In hand-authored file
{
  partial void ValidatePayment (decimal amount)
  {
    if (amount < 100) 
      throw new ArgumentOutOfRangeException ("amount", "Amount too low!");
  }
}
Extended Partial Methods

// The presence of an accessiblity modifier on a partial method denotes an 'extended partial method'.
// Extended partial methods *must* have implementations:

public partial class Test
{
  public partial void M1();    // Extended partial method
  private partial void M2();   // Extended partial method

  public partial bool IsValid (string identifier);
  internal partial bool TryParse (string number, out int result);
}

public partial class Test
{
  public partial void M1() { }
  private partial void M2() {}

  public partial bool IsValid (string identifier) => !string.IsNullOrEmpty (identifier);
  internal partial bool TryParse (string number, out int result) => int.TryParse (number, out result);
}
The nameof operator

int count = 123;
nameof (count).Dump ("count");

nameof (StringBuilder.Length).Dump ("Length property on StringBuilder");

(nameof (StringBuilder) + "." + nameof (StringBuilder.Length)).Dump ("StringBuilder.Length");
