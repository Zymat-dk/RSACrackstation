from Crypto.Util import number

def generatePrimes(size: int) -> tuple:
    """
    Generate two primes of size {size}
    """
    try:
        primes = [number.getStrongPrime(size), number.getStrongPrime(size)]
        return primes, "success", True
    except ValueError:  # Allow for smaller sizes
        primes = [number.getPrime(size), number.getPrime(size)]
        return primes, "success", False
    except:
        return [-1, -1], "error"


def main():
    try:
        size = int(input("Size: "))
    except:
        main()
    numbers, status, is_strong = generatePrimes(size)
    p, q = numbers
    print(f"{'Strong' if is_strong else 'Weak'} key\np: {p}\nq: {q}\nn: {p*q}")


if __name__ == "__main__":
    main()

