open System
open Library
open Npgsql.FSharp

type UserLead = {
    LeadId: int
    UserprofileId: int
    Read: bool
}

let getUserLeads (connectionString: string) =
    connectionString
    |> Sql.connect
    |> Sql.query "SELECT * FROM leads_userlead where userprofile_id = 139"
    |> Sql.execute (fun read ->
        {
            LeadId = read.int "lead_id"
            UserprofileId = read.int "userprofile_id"
            Read = read.bool "read"
        })

[<EntryPoint>]
let main args =
    let connectionString : string =
        Sql.host "localhost"
        |> Sql.database "leads"
        |> Sql.username "postgres"
        |> Sql.password "secret1234"
        |> Sql.port 5432
        |> Sql.formatConnectionString
    let result = getUserLeads(connectionString)
    result |> Seq.iter (fun x -> (printfn
        "LeadId %d UserprofileId %d Read %b" x.LeadId x.UserprofileId x.Read))
    printfn "Found %d leads" result.Length

    0 // return an integer exit code
