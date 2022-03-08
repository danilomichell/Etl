open System
open System.IO

// aqui ficam as tabelas que vão gerar classes no projeto
//let tabelas =
    //[
    //    "folha.cargos"
    //    "folha.carreira"
    //    "folha.colaboradores"
    //    "folha.evolucoes_funcionais"
    //    "folha.folhas_pagamentos"
    //     "folha.grupos_rubricas"
    //    "folha.lancamentos"
    //    "folha.rubricas"
    //    "folha.setores"
    //    "folha.unidades"
    //]

let caminho_appsettings = "Etl.Processamento/appsettings.json"
let projeto_do_contexto = "Etl.Data"
let nome_do_contexto = "FolhaContext"
let diretorio_do_contexto = "Context"
let diretorio_das_entidades = "..\Etl.Data\Domain\Entities"
let projeto_das_entidades = "Etl.Data"
let caminho_string_conexao = "$.ConnectionStrings.FolhaContext" 
let driver_banco_de_dados = "Npgsql.EntityFrameworkCore.PostgreSQL"

// Comandos do terminal
let restore = "dotnet restore"

let run str = 
    System.Diagnostics.Process.Start("CMD.exe","/C " + str).WaitForExit()

//let addRef ref = "dotnet add " + projeto_do_contexto + " reference " + ref

let scaffold_str connection_string= //table_list =
    //let table_str = table_list |> List.map(fun table -> " -t " + table) |> String.concat ""
    [ 
        "dotnet ef dbcontext scaffold \"" + connection_string + "\""
        "Npgsql.EntityFrameworkCore.PostgreSQL"
        "-v"
        "-f"
        "--context-dir " + diretorio_do_contexto
        "-c " + nome_do_contexto
        "-o " + diretorio_das_entidades
        "--no-onconfiguring"
        "--no-pluralize"
        "--project " + projeto_do_contexto
        //table_str
    ] |> String.concat " "

let addPackage pkg = "dotnet add " + projeto_do_contexto + " package " + pkg
//

let scaffold() =
    let conexao = "User ID=danilo;Password=12345678;Host=postgresql-70901-0.cloudclusters.net;Port=19866;Database=folhadb;Pooling=true;"
    run <| scaffold_str conexao //tabelas
    //run <| addRef projeto_das_entidades

let install() =
    run <| addPackage "Microsoft.EntityFrameworkCore.Design"
    run <| addPackage "Microsoft.EntityFrameworkCore.Tools"
    run <| addPackage driver_banco_de_dados
    run restore
    printf "Pacotes instalados, gerar scaffold? (y/n) "
    let resposta = Console.ReadLine()
    if resposta = "y" then 
        scaffold()
    else
        ()

let rec main () =
    printf "Deseja instalar os pacotes para geracao automatica do scaffold? (y/n) "
    let resposta = String.map (Char.ToLower) (Console.ReadLine())

    match resposta with
    | "y" -> install()
    | "n" -> scaffold()
    | _   -> main()

main()
