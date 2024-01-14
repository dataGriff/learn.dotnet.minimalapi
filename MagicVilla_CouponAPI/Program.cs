//using Microsoft.AspNetCore.OpenApi;
// using Microsoft.AspNetCore.Builder;

using MagicVilla_CouponAPI;
using MagicVilla_CouponAPI.Data;
using MagicVilla_CouponAPI.Models;
using MagicVilla_CouponAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ApplicationName = "Coupons"
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingConfig));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/api/coupons", (ILogger<Program> _logger) => { //note the use of the logger dependency injection
    _logger.LogInformation("Getting all coupons");
    return Results.Ok(CouponStore.couponList);
}).WithName("GetCoupons").Produces<IEnumerable<Coupon>>(200);

app.MapGet("/api/coupons/{id:int}", (int id) => {
    return Results.Ok(CouponStore.couponList.FirstOrDefault(c => c.Id == id));
}).WithName("GetCoupon").Produces<Coupon>(200); //note this name is then used in CreatedAtRoute in POST

app.MapPost("/api/coupons", (IMapper _mapper, [FromBody] CouponCreatedDTO coupon_C_DTO) => { //note DTO object usage so only transfer certain data & use of automapper
    if (string.IsNullOrEmpty(coupon_C_DTO.Name))
    {
        return Results.BadRequest("Invalid Id or Coupon Name");
    }
    if (CouponStore.couponList.Any(c => c.Name.ToLower() == coupon_C_DTO.Name.ToLower()))
    {
        return Results.BadRequest("Coupon Name already exists");
    }

    Coupon coupon = _mapper.Map<Coupon>(coupon_C_DTO);
    coupon.Id = CouponStore.couponList.Max(c => c.Id) + 1;
    CouponStore.couponList.Add(coupon);
    CouponDTO couponDTO = _mapper.Map<CouponDTO>(coupon);
    return Results.CreatedAtRoute("GetCoupon", new { id = coupon.Id }, coupon); // note the name of the GET route is used here
}).WithName("CreateCoupon").Accepts<CouponCreatedDTO>("application/json").Produces<CouponDTO>(201).Produces(400); // note difference types of response codes

app.MapPut("/api/coupons/{id:int}", (int id, Coupon coupon) => {
    var index = CouponStore.couponList.FindIndex(c => c.Id == id);
    if (index != -1)
    {
        CouponStore.couponList[index] = coupon;
        return Results.Ok();
    }
    return Results.NotFound();
});

app.MapDelete("/api/coupons/{id:int}", (int id) => {
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