#!/bin/bash
while :
do 
  PORTS=$(lsof -i:1433)
  echo "$PORTS"
  if [ -z "$PORTS" = '' ]; then
    echo 'Database is down, trying to reconnect to run initial migrations!'
    sleep 5s
  else
    docker exec sqlserver_hackathon /opt/mssql-tools18/bin/sqlcmd -S localhost,1433 \
     -U sa -P Password123! -d master -i initdb/initial.sql -C; echo "All done!";
    break
  fi
done

echo "Successfuly executed!"