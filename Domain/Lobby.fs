module Domain.Lobby

open Tools.Functional
open Domain.Player
open Domain.Game
open System

let createGuid =
    Guid.NewGuid().ToString()

let join planerName =
    match createPlayerName planerName with
    | Success name -> Success { Id = PlayerId createGuid; Name = name }
    | Failure err -> Failure err

let createGamePlayer designation player =
    { Designation = designation; PlayerId = player.Id }