## General

### Warnings and Errors

- [ ] There are no warnings present in the project.
      *There are warnings about unused variables. Lines with unassigned variables: 15, 26, 27*
- [x] There are no errors present in the project.
- [x] The entire solution can build and run locally on your machine.
- [x] The code works.

### Logging and Debugging

- [x] Logging, where applicable is used to display contextual information during runtime.
- [x] No Console.WriteLine or similar exists.

### Working and Extensibility

- [ ] All class, variable, property and method modifiers are provided with the smallest scope possible.
      *There are three unused variables. Lines with unassigned variables: 15, 26, 27. One is used unnecessary and two variables are never used.*
- [x] There is no dead code (code that cannot be accessed during runtime, *don't* just rely on VS).
      *No dead code during runtime, however there are methods for unit testing that are not accessible during runtime.*
- [x] Code is not repeated or duplicated (use loops instead of repitition!).

### Readability

- [ ] The code is easy to understand.
      *PlayerMovement code is present in only one class file. It would be easier to understand if the code would be divided into several classes. Since the project is still in making, there are a lot of commented code that might be useful in the future.*
- [ ] Constant variables have been used where applicable.
      *Variables that describe game object forces could be set to constants*
- [x] There are *no* complex long boolean expressions (i.e; `x = isMatched ? shouldMatch ? doesMatch ? blahBlahBlah`).
- [x] There are *no* negatively named booleans (i.e; `notMatch`should be `isMatch` and the logical negation operator (`!`) should be used.

### Other

- [ ] There are no empty blocks of code or unused variables.
        *No dead code during runtime, however there are methods for unit testing that are not accessible during runtime.*
- [x] Floating point numbers are not compared for equality, except in the case where a data structure requires it, such as vector comparison.
        *Float values are mostly used in vectors, but in line 233 it is also used for comparison in integrated method*
- [x] Loops have a set length and correct termination conditions.
- [x] No object exists longer than necessary
- [x] Law of Demeter is not violated

## Design

- [x] Will developers be able to modify the program to fit changing requirements?
- [x] Is there hard-coded data that should be modifiable?
- [x] Is the program sufficiently modular? Will modifications to one part of the program require modifications to others?
- [x] Do you, the reviewer, understand what the code does?
- [x] Does the program reinvent the wheel?
  - [x] Can parts of the functionality be replaced with the standard library?
  - [x] Can parts of the functionality be replaced with a viable third party library?
- [x] Is all of the functionality necessary? Could parts be removed without changing the performance?
- [ ] If code is commented, could the code be rewritten so that the comments aren't necessary?
    *There are some methods (for example WallJump method) that should be commented to clearly explain its functionality.*

## Styling and Coding Conventions

A great tip for below is to include [Roslynator 2019](https://marketplace.visualstudio.com/items?itemName=josefpihrt.Roslynator2019) in your project(s). 

- [x] Any new files have been named consistently and spelt correctly.
- [x] Any and all members have been named simply and if possible, short and to the point (prefer `isMatch` over `isPatternMatched`).
- [ ] There is _no_ commented out code.
      *There are commented out code that might be useful for future improvements* 

## Testing

- [x] There are unit tests provided to accommodate the changes.
- [ ] The provided unit tests have a code coverage of _at least_ 80%.
      *The code coverage is 74,1%*
- [ ] The tests follow the correct styling (MethodName_Should_ExpectedBehaviour_When_StateUnderTest)
      *Tests names are easy to understand but does not follow the correct styling* 
- [x] The tests follow the AAA (Arrange, act and assert) methodology and are clearly commented.
      *In MovementTests the tests follow the AAA methodology but not every test is clearly commented*
- [x] All tests pass locally on your machine
      *Only generated tests do not pass* 

## Exceptions & Error Handling

- [ ] Constructors, methods, etc do not accept `null` (unless otherwise stated so in the documentation with relevant context)
      *Some methods do not check if given value is null*
- [x] No printing of exception throwing is present.
- [x] Are the error messages, if any, informative?
      *There aren't any error messages* 
- [ ] Does the program produce a reasonable amount of logging for its function?
      *In PlayerMovement class there aren't any logging messages for functions*
