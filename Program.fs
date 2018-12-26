// Learn more about F# at http://fsharp.org

open System
open Model

let playGame = 
    start
    |> play { Position = { Vertical = VerticalPosition.Top; Horizontal = HorizontalPosition.Left }; Player = Player.X }
    |> play { Position = { Vertical = VerticalPosition.Center; Horizontal = HorizontalPosition.Center }; Player = Player.O }
    |> play { Position = { Vertical = VerticalPosition.Bottom; Horizontal = HorizontalPosition.Left }; Player = Player.X }
    |> play { Position = { Vertical = VerticalPosition.Center; Horizontal = HorizontalPosition.Left }; Player = Player.O }


[<EntryPoint>]
let main argv =
    let playedGame = playGame
    printfn "%A" playedGame
    0 // return an integer exit code
