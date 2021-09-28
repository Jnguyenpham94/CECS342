// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System

type AccountStatus = 
    | Empty of int
    | Balance of int
    | OverDrawn of int

type BankAccount = {name: string; account: AccountStatus; creditLimit: int option }

let withdraw bankAccount requestInt =
    ()

[<EntryPoint>]
let main argv =
    0 // return an integer exit code