﻿using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using System.Security.Claims;

namespace IWantApp.Endpoints.Employees;

public class EmployeeGetAll
{
    public static string Template => "/employees";
    public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
    public static Delegate Handle => Action;

    public static IResult Action(int page, int rows, IConfiguration configuration)
    {
        var db = new SqlConnection(configuration["ConnectionStrings: IWantDb"]);
        var employees = db.Query<EmployeeResponse>(
            @"select Email, ClaimValue as Name
                from AspNetUsers u inner
                join AspNetUserClaims c
                on u.id = c.UserId and claimtype = 'Name'"
        );

        return Results.Ok(employees);
    }

}
