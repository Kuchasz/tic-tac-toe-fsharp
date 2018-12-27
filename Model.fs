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

let play (board: Board) (move: Move) =
    Map.add move.Position (Settled move.Player) board

let createPosition v h =
    (v, h)

let createEmptyBoard =
    [createPosition VerticalPosition.Top; createPosition VerticalPosition.Center; createPosition VerticalPosition.Bottom]
        |> Seq.map (fun v -> [v Left; v Center; v Right]) 
        |> Seq.fold Seq.append Seq.empty
        |> Seq.map (fun (v, h) -> ({Vertical = v; Horizontal = h}, Empty))
        |> Map.ofSeq

let start = createEmptyBoard