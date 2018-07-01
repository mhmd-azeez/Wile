# Wile
Solving problems nobody has is probably a programmer's favourite hobby, at least it's my favorite hobby. So I am now going to create a json parser for .Net although there are excelent [implementations](https://github.com/JamesNK/Newtonsoft.Json) already. The parser is named after [Wile E. Coyote](https://en.wikipedia.org/wiki/Wile_E._Coyote_and_the_Road_Runner), thus I suggest you lower your expectations.

The implementation is influenced by [Bob Nystrom's book](http://www.craftinginterpreters.com). It is an excellent book about making interpreters, make sure you check it out if that's the sort of thing that you're interested in.

## End goal
Because this is just a side project, I don't expect this parser to be able to parse 100% of the [spec](https://tools.ietf.org/html/rfc7159). But I'll try to make it as complete as possible. These are the components that I plan to implement: 

 - [X] Scanner
 - [X] Parser
 - [ ] Simple Serializer
 - [ ] Simple Deserializer
