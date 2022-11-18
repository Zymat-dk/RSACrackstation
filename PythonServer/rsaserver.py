#!/usr/bin/env python3

from http.server import BaseHTTPRequestHandler, HTTPServer
from socketserver import ThreadingMixIn
from urllib.parse import parse_qs, urlparse
import json

from keygen import generatePrimes
from smallE import smallE

MAX_VAL = 2048
DEFAULT_VAL = 1024


class RequestHandler(BaseHTTPRequestHandler):
    def do_GET(self):
        parsed_path = urlparse(self.path)
        params = parse_qs(parsed_path.query)
        params = parse_params(params)
        self.send_response(200)
        self.end_headers()

        # Set default response
        response = {
            'method': self.command,
            'params': params,
            'request_version': self.request_version,
            'protocol_version': self.protocol_version,
        }

        function_response = generatePrimes(params['size'])  # Get a response from the function
        response.update(function_response)  # Add function response to the response

        self.wfile.write(json.dumps(response).encode())  # Write the response to the client
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


class ThreadedHTTPServer(ThreadingMixIn, HTTPServer):
    """Handle requests in a separate thread."""


if __name__ == '__main__':
    server = ThreadedHTTPServer(('localhost', 8080), RequestHandler)
    print("Starting server, use <Ctrl-C> to stop")
    server.serve_forever()
