using Confluent.Kafka;
using LCE.Core.EventBus;
using LCE.Core.Events;
using LCE.Core.Handlers;
using LCE.Core.Infrastructure;
using LCE.Core.Repository;
using LCE.Domain.Commands;
using LCE.Domain.Domain;
using LCE.Domain.Events;
using LCE.Infra.Dispatchers;
using LCE.Infra.EventBus;
using LCE.Infra.Handler;
using LCE.Infra.Repository;
using LCE.Infra.Stores;
using MongoDB.Bson.Serialization;
using Post.Cmd.Infrastructure.Config;


var builder = WebApplication.CreateBuilder(args);




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();