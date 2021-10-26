open System

type AccountStatus = 
    | Empty of int //Empty 0
    | Balance of int //positive balance
    | OverDrawn of int //negative balance

type BankAccount = {name: string; account: AccountStatus; creditLimit: int option}

let isWealthy bankAccount =
    ()

let findOverdrawn bankAccount =
    ()

let largerAmount a b =
    ()

let accountAmounts bankAccounts =
    ()

let amountsWhere =
    ()

let combineAccounts accountStatus =
    ()

let wealthiestAccount bankAccount =
    ()

[<EntryPoint>]
let main argv =
    let neal = {name = "Neal Terrell"; account = Balance 100; creditLimit = None}
    let dave = {name = "Dave Davidson"; account = OverDrawn 200; creditLimit = None}
    let tom = {name = "Tom Thompson"; account = Balance 200000; creditLimit = Some 500}
    let jackie = {name = "Jackie Jackson"; account = Empty 0; creditLimit = None}

    0