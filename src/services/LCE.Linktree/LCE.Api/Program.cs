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

// Add services to the container.
BsonClassMap.RegisterClassMap<BaseEvent>();
BsonClassMap.RegisterClassMap<LinkAddedEvent>();
BsonClassMap.RegisterClassMap<LinkRemovedEvent>();
BsonClassMap.RegisterClassMap<LinktreeRemovedEvent>();
BsonClassMap.RegisterClassMap<LinktreeUpdatedEvent>();
BsonClassMap.RegisterClassMap<LinkUpdatedEvent>();


builder.Services.Configure<MongoDbConfig>(builder.Configuration.GetSection(nameof(MongoDbConfig)));
builder.Services.Configure<ProducerConfig>(builder.Configuration.GetSection(nameof(ProducerConfig)));
builder.Services.AddScoped<IEventStoreRepository, EventStoreRepository>();
builder.Services.AddScoped<IEventPublish, Kafka>();
builder.Services.AddScoped<IEventStore, EventStore>();
builder.Services.AddScoped<IEventSourcingHandler<LinktreeAggregate>, EventSourcingHandler>();
builder.Services.AddScoped<ICommandHandler, CommandHandler>();

var commandHandler = builder.Services.BuildServiceProvider()
    .GetRequiredService<ICommandHandler>();

var dispatcher = new CommandDispatcher();
dispatcher.RegisterHandler<AddedLinkCommand>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<DeleteLinkCommand>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<DeleteLinktreeCommand>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<EditLinkCommand>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<EditLinktreeCommand>(commandHandler.HandleAsync);
dispatcher.RegisterHandler<NewLinktreeCommand>(commandHandler.HandleAsync);
builder.Services.AddSingleton<ICommandDispatcher>(_ => dispatcher);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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