module Lobby

open SimpleFp
open Player

let join planerName =
    match createPlayerName planerName with
    | Success name -> Success { Id = PlayerId "blah"; Name = name }
    | Failure err -> Failure err