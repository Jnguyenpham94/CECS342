// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System

type AccountStatus = 
    | Empty of int
    | Balance of int
    | OverDrawn of int

type BankAccount = {name: string; account: AccountStatus; creditLimit: int option}

//TODO: withdraw function
let withdraw bankAccount requestInt =
    ()

[<EntryPoint>]
let main argv =
    let m_burns = {name = "Montgomery Burns"; account = Balance 100000; creditLimit = Some 10000}
    let neal = {name = "Neal Terrell"; account = OverDrawn 100; creditLimit = None}
    let bob = {name = "Robert Dugalle"; account = Empty 0; creditLimit = Some 1000}
    0 // return an integer exit code