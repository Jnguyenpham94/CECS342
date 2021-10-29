open System

type AccountStatus = 
    | Empty of int //Empty 0
    | Balance of int //positive balance
    | OverDrawn of int //negative balance

type BankAccount = {name: string; account: AccountStatus; creditLimit: int option}

//take 1 BankAccount 
//RETURNS true if account Balance > 100,000
//PREDICATE
let isWealthy bankAccount =
    match bankAccount.account with
    |Balance b -> if b > 100000 then
                    true
                  else
                    false
    | _ -> false

//list of BankAccounts 
//RETURNS new list containing only accounts that are OverDrawn
//HINT: List.filter
//PREDICATE
let findOverdrawn bankAccount =
    bankAccount |> List.filter (fun a -> match a.account with
                                            | OverDrawn o-> true
                                            | _ -> false)

//Helper largerAmount
//converts account amount to int: Empty = 0; Balance = pos; OverDrawn = neg
let convertInt a =
    match a.account with
    |Empty e-> 0
    |Balance b-> b
    |OverDrawn o-> o * (-1)

//Helper combineAccounts
//converts accountStatus -> int
let convertAccountStatus a =
    match a with
    |Empty e-> 0
    |Balance b-> b
    |OverDrawn o-> o * (-1)

//Helper combineAccounts
//converts int -> AccountStatus #
let convertIntAccount a =
    if a > 0 then
        Balance a
    elif a < 0 then
        OverDrawn (abs a)
    else
        Empty a

//2 BankAccounts
//RETURNS whichever has larger $. OverDrawn : negative $; Empty: 0
//HINT: write helper to convert account to int
//PREDICATE
let largerAmount a b =
    let testA = convertInt a
    let testB = convertInt b
    if testA > testB then
        a
    else
        b

//list of BankAccounts
//RETURNS new list containing $ 
//HINT: List.map
let accountAmounts bankAccounts =
    bankAccounts |> List.map (fun a -> convertInt a)

//predicate of tpe (BankAccount->bool), list of BankAccounts
//RETURNS new list of $ amounts of accounts that satisfy(true) the predicate
let amountsWhere predicate bankAccounts =
    bankAccounts |> List.filter(fun a -> predicate a) |> List.map (fun b -> convertInt b)

//list of AccountStatus
//RETURNS sum of $ amounts: Empty is 0; OverDrawn with abs of $; Balance other
//HINT: List.map, List.reduce, then some simple logic.
//Or if you're really slick, write a helper function to combine two account statuses, then reduce with that function.
//Accountstatus list-> int list -> int -> AccountStatus 
let combineAccounts accountStatus =
    accountStatus |> List.map(fun a -> convertAccountStatus a) |> List.reduce(+) |> convertIntAccount


//list of BankAccounts
//RETURNS largest $ BankAccount obj; if 2 have same amount RETURNS first one
//HINT: List.reduce or List.fold. Do not assume that every account has a different money amount. Do not map the accounts into money amounts, find the largest of those, and then go back to try and find the account with that money amount. That is way too much work. Consider how largerAmount can help.
let wealthiestAccount bankAccount =
    ()

[<EntryPoint>]
let main argv =
    let neal = {name = "Neal Terrell"; account = Balance 100; creditLimit = None}
    let dave = {name = "Dave Davidson"; account = OverDrawn 200; creditLimit = None}
    let tom = {name = "Tom Thompson"; account = Balance 200000; creditLimit = Some 500}
    let jackie = {name = "Jackie Jackson"; account = Empty 0; creditLimit = None}

    isWealthy neal |> printfn "%O" //false
    isWealthy tom |> printfn "%O" //true
    findOverdrawn [neal; dave; tom] |> printfn "%A" //[{name = "Dave Davidson"; account = Overdrawn 200; creditLimit = None}]
    largerAmount neal dave |> printfn "Larger amount:\n%O" //{name = "Neal Terrell"; account = Balance 100; creditLimit = None}
    accountAmounts [neal; dave; tom; jackie] |> printfn "Account amounts:\n%A"
    amountsWhere isWealthy [neal; dave; tom; jackie] |> printfn "Amount(s) where predicate:\n %A"
    combineAccounts [Balance 100; OverDrawn 200; Empty 0; Balance 1100; Balance 100; OverDrawn 300] |> printfn "Combine accounts result is : %O" //Balance 800
    combineAccounts [Balance 100; OverDrawn 100; Empty 0] |> printfn "Combine accounts result is : %O" //Empty 0
    combineAccounts [Balance 200; OverDrawn 100; Empty 0; OverDrawn 300] |> printfn "Combine accounts result is : %O" //OverDrawn 200

    0