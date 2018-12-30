﻿open SimpleFp
open Model
let moves = [
    { Position = { Vertical = VerticalPosition.Top; Horizontal = HorizontalPosition.Left }; Player = Player.X }
    { Position = { Vertical = VerticalPosition.Center; Horizontal = HorizontalPosition.Center }; Player = Player.O }
    { Position = { Vertical = VerticalPosition.Bottom; Horizontal = HorizontalPosition.Left }; Player = Player.X }
    { Position = { Vertical = VerticalPosition.Center; Horizontal = HorizontalPosition.Left }; Player = Player.O }
]

let playGame: Result<Board, string> = 
    moves |> Seq.fold (fun acc item -> (bind (play item)) acc)  start

[<EntryPoint>]
let main argv =
    let playedGame = playGame
    printfn "%A" playedGame
    0