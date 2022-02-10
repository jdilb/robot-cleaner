# robot-cleaner

## Background
The robot cleaner is cutting edge, next gen AI technology that, 
with the help of several blockchains and a lot of big data, will do some cleaning and reporting.

## How to run

1. clone the repository
2. cd into the Cint.RobotCleaner folder
3. run `dotnet build`
4. run `dotnet run`

## Some notes on the implementation

The implementation of the robot cleaner is more extensive than needed for this basic functionality. However, a scalable, testable architecture was
chosen to showcase several aspects of solution design. 

Dependency injection was used heavily as it is a very important means of IoC that brings testability, decoupling and other benefits. Again, for this 
assignment DI was mostly used for demo purposes.

Another focus was on code readability. There may be many comments in the code, but none of them explain 'what' is happening. They only reflect 'why' I have 
chosen a specific way of implementation and would not be as much clutter in an actual project. Instead of commenting on 'why', I opted for clear variable and 
function naming instead, even resorting to local functions to provide as much clarity for the reader as possible. Methods are kept very short. Many of these
ideas are in line with [clean code](https://www.amazon.se/Clean-Code-Handbook-Software-Craftsmanship/dp/0132350882/ref=sr_1_5?keywords=clean+code&qid=1644522986&sr=8-5) principles.

The classes were designed in a way so they would serve one specific purpose, keeping the functionality inside small and testable. The solution was built
in individual blocks (as reflected by the pull requests). This implies that the solution could have been built by several engineers in parallel
without meeting in merge hell.

The line coverage by unit tests is >90%.

## Thanks
Whatever you think about this implementation, I'm happy to hear it. 

Thank you very much for reviewing!