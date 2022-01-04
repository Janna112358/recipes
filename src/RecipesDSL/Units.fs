module Units


type Unit = 
    {
        Symbol: string 
        Name:   string 
    }
with 
    static member U symbol name = 
        { 
            Symbol  = symbol
            Name    = name
        }
    member this.toString = 
        this.Symbol


// note: could be moved to db later
let units = 
    [
        "#", "number"
        "kg", "kilo"
        "g", "gram"
        "mL", "mili liter"
        "tbsp", "table spoon"
        "tsp", "teaspoon"
    ]
    |> List.map ( fun (symbol, name) -> 
        { Symbol = symbol; Name = name } )

let tryFindUnit (symbol: string) = 
    units 
    |> List.tryFind (fun u -> u.Symbol = symbol)

let tryFindUnitName (name: string) = 
    units 
    |> List.tryFind (fun u -> u.Name = name)

type Unit with 
    static member create (symbol: string) = 
        match tryFindUnit symbol with 
        | None -> failwith (sprintf "No unit found with symbol [%s]" symbol)
        | Some u -> u
        