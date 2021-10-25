open System

type AccountStatus = 
    | Empty of int //Empty 0
    | Balance of int //positive balance
    | OverDrawn of int //negative balance

type BankAccount = {name: string; account: AccountStatus; creditLimit: int option}

[<EntryPoint>]
let main argv =
    //let m_burns = {name = "Montgomery Burns"; account = Balance 100000; creditLimit = Some 10000}
    //let bob = {name = "Robert Dugalle"; account = Empty 0; creditLimit = Some 200}
    let neal = {name = "Neal Terrell"; account = Balance 100; creditLimit = None}
    let dave = {name = "Dave Davidson"; account = OverDrawn 200; creditLimit = Some 0}
    let tom = {name = "Tom Thompson"; account = Balance 100; creditLimit = Some 500}
    0