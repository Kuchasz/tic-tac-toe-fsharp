open SimpleFp
open Game
open Player

let playerX = { Designation = PlayerDesignation.X; PlayerId = (PlayerId "000000-0000-0000-000000") }
let playerO = { Designation = PlayerDesignation.O; PlayerId = (PlayerId "000000-0000-0000-000001") }

let moves = [
    { Position = { Vertical = VerticalPosition.Top; Horizontal = HorizontalPosition.Left }; Player = playerX }
    { Position = { Vertical = VerticalPosition.Center; Horizontal = HorizontalPosition.Center }; Player = playerO }
    { Position = { Vertical = VerticalPosition.Bottom; Horizontal = HorizontalPosition.Left }; Player = playerX }
    { Position = { Vertical = VerticalPosition.Center; Horizontal = HorizontalPosition.Left }; Player = playerO }
]

let playGame: Result<Game, string> = 
    moves |> Seq.fold (fun acc item -> (bind (play item)) acc)  start

[<EntryPoint>]
let main argv =
    let playedGame = playGame
    printfn "%A" playedGame
    0