using System;
using System.Text.Json;
using FunctionAppAcoes.Models;
using StackExchange.Redis;

namespace FunctionAppAcoes.Data
{
    public static class AcoesRepository
    {
        private static ConnectionMultiplexer _CONEXAO_REDIS =
            ConnectionMultiplexer.Connect(
                Environment.GetEnvironmentVariable("BaseAcoesRedis"));

        public static void Save(Acao acao)
        {
            acao.UltimaAtualizacao = DateTime.Now;
            var dbRedis = _CONEXAO_REDIS.GetDatabase();
            dbRedis.StringSet(
                "ACAO-" + acao.Codigo,
                JsonSerializer.Serialize(acao),
                expiry: null);
        }
    }
}