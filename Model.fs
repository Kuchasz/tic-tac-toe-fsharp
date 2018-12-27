module Model

type VerticalPosition = Top | Center | Bottom
type HorizontalPosition = Left | Center | Right

type Position = {
    Vertical: VerticalPosition
    Horizontal: HorizontalPosition
}

type Player = 
    | X 
    | O

type FieldState = 
    | Empty 
    | Player of Player

type Field = {
    FieldState: FieldState
    Position: Position
}

type Move = {
    Player: Player
    Position: Position
}

type Board = {
    Fields: Field list
}

let play board move =
    let playedField = { FieldState = Player move.Player; Position = move.Position }
    { Fields = board.Fields @ [playedField] }

let start = 
    { Fields = []}