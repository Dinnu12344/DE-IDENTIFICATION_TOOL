import hashlib

def generate_hash(value, key):
  
    # Convert both the value and key to strings
    value_str = str(value)
    key_str = str(key)
    # Combine the value and key
    combined = value_str + key_str
    # Use SHA-256 hash function to generate the hash key
    hash_key = hashlib.sha256(combined.encode()).hexdigest()
    return hash_key

# Example usage:
value = "example_value"
key = "user_provided_key"
hash_result = generate_hash(value, key)
print(f"Hash Result: {hash_result}")
