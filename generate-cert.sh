#!/bin/bash
set -e

CERT_DIR="./src/LoanSimulatorWebAPI/certs"
CERT_PATH="$CERT_DIR/localhost.pfx"
CERT_PASS="123"

if [ ! -f "$CERT_PATH" ]; then
    echo "🔑 Gerando certificado de desenvolvimento..."
    dotnet dev-certs https -ep "$CERT_PATH" -p "$CERT_PASS"
    dotnet dev-certs https --trust
    echo "✅ Certificado gerado e confiável em $CERT_PATH"
else
    echo "✅ Certificado já existe em $CERT_PATH"
fi
