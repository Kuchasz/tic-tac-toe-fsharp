module Domain.Lobby

open Tools.Functional
open Domain.Player

let join planerName =
    match createPlayerName planerName with
    | Success name -> Success { Id = PlayerId "blah"; Name = name }
    | Failure err -> Failure err