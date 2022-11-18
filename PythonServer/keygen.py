from Crypto.Util import number


def generatePrimes(size: int) -> dict:
    """
    Generate two primes of size {size}
    """
    response = {"numbers": [-1, -1], "status": "error", "is_strong": False}
    try:
        primes = [number.getStrongPrime(size), number.getStrongPrime(size)]
        response = {"numbers": primes, "status": "success", "is_strong": True}
    except ValueError:  # Allow for smaller sizes
        primes = [number.getPrime(size), number.getPrime(size)]
        response = {"numbers": primes, "status": "success", "is_strong": False}
    except:
        pass
    return response


def main():
    try:
        size = int(input("Size: "))
    except:
        main()
    response = generatePrimes(size)
    p, q = response["numbers"]
    print(f"{'Strong' if response['is_strong'] else 'Weak'} key\np: {p}\nq: {q}\nn: {p * q}")


if __name__ == "__main__":
    main()
