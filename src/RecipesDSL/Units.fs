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
        "pinch", "pinch"
    ]
    |> List.map (fun (symbol, name) -> Unit.U symbol name)
