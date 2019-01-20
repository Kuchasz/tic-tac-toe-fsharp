open Tools.Functional
open Domain.Game
open Domain.Player
open Domain.Lobby

let planerJohn = join "John Doe"
let planerDory = join "Dory Cutsky"

let createMoves playerX playerO =
    match (playerX, playerO) with
    | (Success gamePlayerX, Success gamePlayerO) ->
        let gamePlayerX = createGamePlayer PlayerDesignation.X gamePlayerX
        let gamePlayerO = createGamePlayer PlayerDesignation.O gamePlayerO
        Success [
            { Position = { Vertical = VerticalPosition.Top; Horizontal = HorizontalPosition.Left }; Player = gamePlayerX }
            { Position = { Vertical = VerticalPosition.Center; Horizontal = HorizontalPosition.Center }; Player = gamePlayerO }
            { Position = { Vertical = VerticalPosition.Bottom; Horizontal = HorizontalPosition.Left }; Player = gamePlayerX }
            { Position = { Vertical = VerticalPosition.Center; Horizontal = HorizontalPosition.Left }; Player = gamePlayerO }
        ]
    | _ -> Failure "Can't create moves"

let playMoves moves = 
    moves |> Seq.fold (fun acc item -> (bind (play item)) acc)  start

let createAllGameStory = 
    let moves = createMoves planerJohn planerJohn
    match moves with
    | Success moves -> playMoves moves
    | Failure err -> Failure err



[<EntryPoint>]
let main argv =
    let playedGame = createAllGameStory
    printfn "%A" playedGame
    0