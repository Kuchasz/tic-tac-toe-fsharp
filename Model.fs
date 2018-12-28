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

type Board = Map<Position, FieldState>

let play (board: Board) move =
    match board.Item move.Position with
    | Empty -> Map.add move.Position (Settled move.Player) board
    | Settled _ -> board

let createEmptyField v h =
    ({Vertical = v; Horizontal = h}, Empty)

let createEmptyBoard =
    [VerticalPosition.Top; VerticalPosition.Center; VerticalPosition.Bottom]
        |> Seq.collect (fun v -> [Left; Center; Right] |> Seq.map (createEmptyField v)) 
        |> Map.ofSeq

let start = createEmptyBoard