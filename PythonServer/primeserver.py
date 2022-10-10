#!/usr/bin/env python3

from Crypto.Util import number
from http.server import BaseHTTPRequestHandler, HTTPServer
from urllib.parse import parse_qs, urlparse
import json
from socketserver import ThreadingMixIn
import threading


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
    Convert {size: ['4']} to {size: 4} and use 2048 as default case
    """

    size = params.get("size", ["2048"])[0]
    try:
        size = int(size)
    except ValueError:
        size = 2048
    params["size"] = size
    return params


def generatePrimes(size: int) -> tuple:
    """
    Generate two primes of size {size}
    """
    try:
        return [number.getPrime(size), number.getPrime(size)], "success"
    except:
        return [-1, -1], "error"


class ThreadedHTTPServer(ThreadingMixIn, HTTPServer):
    """Handle requests in a separate thread."""


if __name__ == '__main__':
    server = ThreadedHTTPServer(('localhost', 8080), RequestHandler)
    print("Starting server, use <Ctrl-C> to stop")
    server.serve_forever()
