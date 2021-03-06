module Domain.Game

open Tools.Functional
open Domain.Player

type VerticalPosition = Top | Center | Bottom
type HorizontalPosition = Left | Center | Right

type Position = {
    Vertical: VerticalPosition
    Horizontal: HorizontalPosition
}

type PlayerDesignation = X | O

type GamePlayer = {
    Designation: PlayerDesignation
    PlayerId: PlayerId
}

type GameStatus = 
    | Init 
    | InProgress 
    | Strike 
    | Won of GamePlayer

type FieldState = 
    | Empty 
    | Settled of GamePlayer

type Move = {
    Player: GamePlayer
    Position: Position
}

type Game = {
    Map: Map<Position, FieldState>
    LastPlayer: GamePlayer option
    Status: GameStatus
}

let validatePositionEmptyness ((game: Game), (move: Move)) = 
    match game.Map.Item move.Position with
    | Empty -> Success (game, move)    
    | _ -> Failure "Position already settled up"

let validatePreviousPlayer ((game: Game), (move: Move)) = 
    match game.LastPlayer = Some move.Player with
    | false -> Success (game, move)    
    | true -> Failure "Player already played"

let validateGameCanProceed ((game: Game), (move: Move)) = 
    match game.Status with
    | Init -> Success (game, move)    
    | InProgress -> Success (game, move)
    | Strike -> Failure "Game already ended with strike"
    | Won player -> Failure (sprintf "Game already won by %A" player)

let validation (game, move) = 
    validatePositionEmptyness (game, move)
    >>= validatePreviousPlayer
    >>= validateGameCanProceed

let settlePlayer boardMap (position: Position) player =
    Map.add position (Settled player) boardMap

let play move (game: Game) =
    match validation (game, move) with
    | Success (game, move) -> Success { game with LastPlayer = (Some move.Player); Map = settlePlayer game.Map move.Position move.Player}
    | Failure f -> Failure f

let createEmptyField v h =
    ({Vertical = v; Horizontal = h}, Empty)

//F(x).map(f) === F(f).ap(F(x))

let createEmptyGame: Result<Game, string> =
    let boardMap = 
        [VerticalPosition.Top; VerticalPosition.Center; VerticalPosition.Bottom]
            |> Seq.collect (fun v -> [Left; Center; Right] |> Seq.map (createEmptyField v)) 
            |> Map.ofSeq

    Success ({ Status = Init; Map = boardMap; LastPlayer = None})

let start = createEmptyGame