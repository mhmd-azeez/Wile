# Wile
Solving problems nobody has is probably a programmer's favourite hobby, at least it's my favorite hobby. So I am now going to create a json parser/generator for .Net although there are excellent [implementations](https://github.com/JamesNK/Newtonsoft.Json) already. The project is named after [Wile E. Coyote](https://en.wikipedia.org/wiki/Wile_E._Coyote_and_the_Road_Runner) :wolf:.

The implementation is heavily influenced by [Bob Nystrom's book](http://www.craftinginterpreters.com). It is an excellent book about making interpreters, make sure you check it out if that's the sort of thing that you're interested in.

It turns out Json is an excellent languge to make parsers for, because it's very small (its [spec]((https://tools.ietf.org/html/rfc8259)) is under 3,500 words) although it does have [its quirks](http://seriot.ch/parsing_json.php). But the goal of this project is to have something that works 95% of the time so we should be fine, this is just a learning practice after all.

## End goal
The end result should be compliant with [RFC 8259](https://tools.ietf.org/html/rfc8259) (At least as much as possible). These are the components that I plan to implement: 

 - [X] Scanner
 - [X] Parser
 - [X] Generator
 - [ ] Simple Serializer
 - [ ] Simple Deserializer
