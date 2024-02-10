// If you really want to use a keyword as an identifier, you can do so with the @ prefix.
// This can be useful for language interoperability.

int @class = 123;
string @namespace = "foo";

// The identifiers below are examples of *contextual* keywords, so we can use them without conflict:

int add = 3;
bool ascending = true;
int yield = 45;


// Statements can span multiple lines, thanks to the semicolon terminator:

Console.WriteLine
  (1 + 2 + 3 + 4 + 5 + 6 + 7 + 8 + 9 + 10);

int x = 3;   // Single-line comment

int y = 3;   /* This is a comment that
        spans two lines */
