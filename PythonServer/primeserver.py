#!/usr/bin/env python3

from Crypto.Util import number
from http.server import BaseHTTPRequestHandler, HTTPServer
from urllib.parse import parse_qs, urlparse
import json
from socketserver import ThreadingMixIn
import threading

MAX_VAL = 2048
DEFAULT_VAL = 1024

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
            numbers = [-1, -1]
            status = "error"

        self.wfile.write(json.dumps({
            'method': self.command,
            'params': params,
            'request_version': self.request_version,
            'protocol_version': self.protocol_version,
            'status': status,
            'numbers': numbers
        }).encode())
        return


def parse_params(params: dict) -> dict:
    """
    Convert {size: ['4']} to {size: 4} and use DEFAULT_VAL as default case
    """

    size = params.get("size", ["1024"])[0]
    try:
        size = int(size)
    except ValueError:
        size = DEFAULT_VAL
    if size < 2 or size > MAX_VAL:
        size = DEFAULT_VAL
    params["size"] = size
    return params


def generatePrimes(size: int) -> tuple:
    """
    Generate two primes of size {size}
    """
    try:
        primes = [number.getPrime(size), number.getPrime(size)]
        return primes, "success"
    except:
        return [-1, -1], "error"


class ThreadedHTTPServer(ThreadingMixIn, HTTPServer):
    """Handle requests in a separate thread."""


if __name__ == '__main__':
    server = ThreadedHTTPServer(('localhost', 8080), RequestHandler)
    print("Starting server, use <Ctrl-C> to stop")
    server.serve_forever()
