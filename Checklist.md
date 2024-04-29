## General

### Warnings and Errors

- [ ] There are no warnings present in the project.
- [ ] There are no errors present in the project.
- [ ] There are no warnings that have been supressed.
- [ ] The entire solution can build and run locally on your machine.
- [ ] The code works.

### Logging and Debugging

- [ ] Logging, where applicable is used to display contextual information during runtime.
- [ ] All debuging code is absent, where applicable see if an issue should be filed, instead.
- [ ] No Console.WriteLine or similar exists.

### Working and Extensibility

- [ ] All class, variable, property and method modifiers are provided with the smallest scope possible.
- [ ] There is no dead code (code that cannot be accessed during runtime, *don't* just rely on VS).
- [ ] Code is not repeated on duplicated (use loops instead of repitition!).
- [ ] The ideal data structures are used where appropriate (i.e; `Stack` is used instead of `List`) where applicable.
- [ ] Interfaces or abstract classes are passed as parameters to classes and methods instead of concrete implenentations.

### Readability

- [ ] The code is easy to understand.
- [ ] There is no usage of "magic numbers".
- [ ] Enumerations are used in place of constant integers where applicable.
- [ ] `for` loops have been replaced with `foreach` where speed is not a direct issue.
- [ ] The code, where speed is a priority, has been optimized to run as fast as possible.
- [ ] Constant variables have been used where applicable.
- [ ] There are *no* complex long boolean expressions (i.e; `x = isMatched ? shouldMatch ? doesMatch ? blahBlahBlah`).
- [ ] There are *no* negatively named booleans (i.e; `notMatch`should be `isMatch` and the logical negation operator (`!`) should be used.

### Other

- [ ] There are no empty blocks of code or unused variables.
- [ ] Collections are initialised with a specific estimated capacity, where appropriate (if you don't know the size, let it grow).
- [ ] Arrays are checked for out of bounds conditions, just in case.
- [ ] `StringBuilder` is used to concatenate large strings.
- [ ] Floating point numbers are not compared for equality, except in the case where a data structure requires it, such as vector comparison.
- [ ] Loops have a set length and correct termination conditions.
- [ ] Blocks of code inside loops are as small as possible.
- [ ] Order/index of a collection is not modified when it is being looped over, unless _absoutely_ neccessary.
- [ ] No object exists longer than necessary
- [ ] Law of Demeter is not violated
- [ ] Do any of the changes assume anything about input? If so, is the assumption fairly accurate or should it be omitted?

## Design

- [ ] Will developers be able to modify the program to fit changing requirements?
- [ ] Is there hard-coded data that should be modifiable?
- [ ] Is the program sufficiently modular? Will modifications to one part of the program require modifications to others?
- [ ] Do you, the reviewer, understand what the code does?
- [ ] Does the program reinvent the wheel?
  - [ ] Can parts of the functionality be replaced with the standard library?
  - [ ] Can parts of the functionality be replaced with a viable third party library?
- [ ] Is all of the functionality necessary? Could parts be removed without changing the performance?
- [ ] If code is commented, could the code be rewritten so that the comments aren't necessary?

## Styling and Coding Conventions

A great tip for below is to include [Roslynator 2019](https://marketplace.visualstudio.com/items?itemName=josefpihrt.Roslynator2019) in your project(s). 

- [ ] Any new files have bee named consistently and spelt correctly.
- [ ] Any and all members have been named simply and if possible, short and to the point (prefer `isMatch` over `isPatternMatched`).
- [ ] There is _no_ commented out code.
- [ ] All changes follow the styling and coding conventions of the repository, to ensure:

## Testing

- [ ] There are unit tests provided to accommodate the changes.
- [ ] The provided unit tests have a code coverage of _at least_ 80%.
- [ ] The tests follow the correct styling (MethodName_Should_ExpectedBehaviour_When_StateUnderTest)
- [ ] The tests follow the AAA (Arrange, act and assert) methodology and are clearly commented.
- [ ] All tests pass locally on your machine
- [ ] All tests pass on CI.

## Exceptions & Error Handling

- [ ] Constructors, methods, etc do not accept `null` (unless otherwise stated so in the documentation and PR with relevant context)
- [ ] No printing of exception throwing is present.
- [ ] Are the error messages, if any, informative?
- [ ] When an error or exception occurs, does the user get adequate information about what he or she did wrong?
- [ ] Does the program produce a reasonable amount of logging for its function?
