module Player 

open SimpleFp

type PlayerId = PlayerId of string

type PlayerName = PlayerName of string

type Player = {
    Id: PlayerId
    Name: PlayerName
}

let createPlayerName (name: string) =
    match name.Length >=5 with
    | true -> Success (PlayerName name)
    | false -> Failure "Player name is too short"
