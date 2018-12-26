// Learn more about F# at http://fsharp.org

open System
open Model

[<EntryPoint>]
let main argv =
    let game = start
    printfn "%A" game
    let s1 = play game { Position = { Vertical = VerticalPosition.Top; Horizontal = HorizontalPosition.Left }; Player = Player.X }
    printfn "%A" s1
    let s2 = play s1 { Position = { Vertical = VerticalPosition.Center; Horizontal = HorizontalPosition.Center }; Player = Player.O }
    printfn "%A" s2
    let s3 = play s2 { Position = { Vertical = VerticalPosition.Bottom; Horizontal = HorizontalPosition.Left }; Player = Player.X }
    printfn "%A" s3
    let s4 = play s3 { Position = { Vertical = VerticalPosition.Center; Horizontal = HorizontalPosition.Left }; Player = Player.O }
    printfn "%A" s4
    0 // return an integer exit code
