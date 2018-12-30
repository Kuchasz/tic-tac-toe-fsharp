module Model

open SimpleFp

type VerticalPosition = Top | Center | Bottom
type HorizontalPosition = Left | Center | Right

type Position = {
    Vertical: VerticalPosition
    Horizontal: HorizontalPosition
}

type Player = X | O

type GameStatus = 
    | Init 
    | InProgress 
    | Strike 
    | Won of Player

type FieldState = 
    | Empty 
    | Settled of Player

type Move = {
    Player: Player
    Position: Position
}

type Board = {
    Map: Map<Position, FieldState>
    LastPlayer: Player option
    Status: GameStatus
}

let validatePositionEmptyness ((board: Board), (move: Move)) = 
    match board.Map.Item move.Position with
    | Empty -> Success (board, move)    
    | _ -> Failure "Position already settled up"

let validatePreviousPlayer ((board: Board), (move: Move)) = 
    match board.LastPlayer = Some move.Player with
    | false -> Success (board, move)    
    | true -> Failure "Player already played"

let validateGameCanProceed ((board: Board), (move: Move)) = 
    match board.Status with
    | Init -> Success (board, move)    
    | InProgress -> Success (board, move)
    | Strike -> Failure "Game already ended with strike"
    | Won player -> Failure (sprintf "Game already won by %A" player)

let validation (board, move) = 
    validatePositionEmptyness (board, move)
    >>= validatePreviousPlayer
    >>= validateGameCanProceed

let play move (board: Board) =
    match validation (board, move) with
    | Success (board, move) -> Success { board with LastPlayer = (Some move.Player); Map = (Map.add move.Position (Settled move.Player) board.Map)}
    | Failure f -> Failure f

let createEmptyField v h =
    ({Vertical = v; Horizontal = h}, Empty)

let createEmptyBoard: Result<Board, string> =
    let boardMap = 
        [VerticalPosition.Top; VerticalPosition.Center; VerticalPosition.Bottom]
            |> Seq.collect (fun v -> [Left; Center; Right] |> Seq.map (createEmptyField v)) 
            |> Map.ofSeq

    Success ({ Status = Init; Map = boardMap; LastPlayer = None})

let start = createEmptyBoard