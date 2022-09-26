#!/usr/bin/env python3

from Crypto.Util import number
from http.server import BaseHTTPRequestHandler, HTTPServer
from urllib.parse import parse_qs, urlparse
import json

class RequestHandler(BaseHTTPRequestHandler):
    def do_GET(self):
        parsed_path = urlparse(self.path)
        params = parse_qs(parsed_path.query)
        params = parse_params(params)
        self.send_response(200)
        self.end_headers()

        try:
            resp = generatePrimes(params['size'])
            numbers, status = resp
        except:
            raise
            numbers = [-1, -1]
            status = "fail"

        self.wfile.write(json.dumps({
            'method': self.command,
            'params': params,
            'request_version': self.request_version,
            'protocol_version': self.protocol_version,
            'status': status,
            'numbers': numbers
        }).encode())
        return

def parse_params(params: dict) -> int:
    """
    Convert {size: ['4']} to {size: 4} and use 2048 as default case
    """

    size = params.get("size", ["2048"])[0]
    try:
        size = int(size)
    except ValueError:
        size = 2048
    params["size"] = size
    return params

def generatePrimes(size: int) -> list[int]:
    """
    Generate two primes of size {size}
    """
    return [number.getPrime(size), number.getPrime(size)], "success"

if __name__ == '__main__':
    server = HTTPServer(('localhost', 8080), RequestHandler)
    server.serve_forever()
