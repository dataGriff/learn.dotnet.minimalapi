//using Microsoft.AspNetCore.OpenApi;
// using Microsoft.AspNetCore.Builder;

using MagicVilla_CouponAPI.Data;
using MagicVilla_CouponAPI.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/api/coupon", () => {
    return Results.Ok(CouponStore.couponList);
}).WithName("GetCoupons").Produces<IEnumerable<Coupon>>(200);

app.MapGet("/api/coupon/{id:int}", (int id) => {
    return Results.Ok(CouponStore.couponList.FirstOrDefault(c => c.Id == id));
}).WithName("GetCoupon").Produces<Coupon>(200);

app.MapPost("/api/coupon", ([FromBody] Coupon coupon) => {
    if (coupon.Id != 0 || string.IsNullOrEmpty(coupon.Name))
    {
        return Results.BadRequest("Invalid Id or Coupon Name");
    }
    if (CouponStore.couponList.Any(c => c.Name.ToLower() == coupon.Name.ToLower()))
    {
        return Results.BadRequest("Coupon Name already exists");
    }
    coupon.Id = CouponStore.couponList.Max(c => c.Id) + 1;
    CouponStore.couponList.Add(coupon);
    return Results.CreatedAtRoute("GetCoupon", new { id = coupon.Id }, coupon);
}).WithName("CreateCoupon").Accepts<Coupon>("application/json").Produces<Coupon>(201).Produces(400);

app.MapPut("/api/coupon/{id:int}", (int id, Coupon coupon) => {
    var index = CouponStore.couponList.FindIndex(c => c.Id == id);
    if (index != -1)
    {
        CouponStore.couponList[index] = coupon;
        return Results.Ok();
    }
    return Results.NotFound();
});

app.MapDelete("/api/coupon/{id:int}", (int id) => {
    var index = CouponStore.couponList.FindIndex(c => c.Id == id);
    if (index != -1)
    {
        CouponStore.couponList.RemoveAt(index);
        return Results.Ok();
    }
    return Results.NotFound();
});

app.UseHttpsRedirection();

app.Run();