module Model

type VerticalPosition = Top | Center | Bottom
type HorizontalPosition = Left | Center | Right

type Position = {
    Vertical: VerticalPosition
    Horizontal: HorizontalPosition
}

type Player = X | O

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
}

type Result<'TSuccess, 'TFailure> =
    | Success of 'TSuccess
    | Failure of 'TFailure

let bind switchFunction twoTrackInput =
    match twoTrackInput with
    | Success s -> switchFunction s
    | Failure f -> Failure f

let (>>=) twoTrackInput switchFunction =
    bind switchFunction twoTrackInput

let validatePositionEmptyness ((board: Board), (move: Move)) = 
    match board.Map.Item move.Position with
    | Empty -> Success (board, move)    
    | _ -> Failure "Position already settled up"

let validatePreviousPlayer ((board: Board), (move: Move)) = 
    match board.LastPlayer = Some move.Player with
    | false -> Success (board, move)    
    | true -> Failure "Player already played"

let validation twoTrackInput = 
    twoTrackInput
    >>= validatePositionEmptyness
    >>= validatePreviousPlayer

let play move (board: Board) =
    let validationInput = Success (board, move)
    match validation validationInput with
    | Success (board, move) -> Success { LastPlayer = (Some move.Player); Map = (Map.add move.Position (Settled move.Player) board.Map)}
    | Failure f -> Failure f

let createEmptyField v h =
    ({Vertical = v; Horizontal = h}, Empty)

let createEmptyBoard: Result<Board, string> =
    let boardMap = 
        [VerticalPosition.Top; VerticalPosition.Center; VerticalPosition.Bottom]
            |> Seq.collect (fun v -> [Left; Center; Right] |> Seq.map (createEmptyField v)) 
            |> Map.ofSeq

    Success ({ Map = boardMap; LastPlayer = None})

let start = createEmptyBoard