module References

open System 

type Reference = 
    {
        Id:     Guid 
        Text:   string 
    }
with 
    static member create (text: string) = 
        { 
            Id      = Guid.NewGuid()
            Text    = text     
        }
    member this.ref refOrder = 
        refOrder 
        |> Map.tryFind this.Id 
        |> function 
        | None -> "?"
        | Some x -> string x 
        |> sprintf "[%s]"
    member this.display refOrder= 
        sprintf "%s %s" (this.ref refOrder) this.Text
        