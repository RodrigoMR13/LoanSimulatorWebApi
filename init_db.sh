#!/bin/bash

echo "Aguardando SQL Server iniciar..."

while :
do 
  docker exec sqlserver /opt/mssql-tools18/bin/sqlcmd \
    -S localhost -U sa -P 'Password123!' -Q "SELECT 1" -C > /dev/null 2>&1

  if [ $? -eq 0 ]; then
    echo "SQL Server está pronto. Executando script de inicialização..."
    docker exec sqlserver /opt/mssql-tools18/bin/sqlcmd \
      -S localhost -U sa -P 'Password123!' -d master -i ../initdb/initial.sql -C
    echo "Script executado com sucesso!"
    break
  else
    echo "SQL Server ainda não está pronto. Tentando novamente em 10 segundos..."
    sleep 10
  fi
done