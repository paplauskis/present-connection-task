export default function PackageForm({
  sender,
  recipient,
  loading,
  message,
  onChange,
  onSubmit,
}) {
  return (
    <div className="package-form">
      {message && <p className="message">{message}</p>}
      <form onSubmit={onSubmit}>
        <fieldset>
          <legend>Sender Information</legend>
          <input
            type="text"
            name="name"
            placeholder="Name"
            value={sender.name}
            onChange={(e) => onChange(e, 'sender')}
          />
          <input
            type="text"
            name="address"
            placeholder="Address"
            value={sender.address}
            onChange={(e) => onChange(e, 'sender')}
          />
          <input
            type="text"
            name="phone"
            placeholder="Phone"
            value={sender.phone}
            onChange={(e) => onChange(e, 'sender')}
          />
        </fieldset>

        <fieldset>
          <legend>Recipient Information</legend>
          <input
            type="text"
            name="name"
            placeholder="Name"
            value={recipient.name}
            onChange={(e) => onChange(e, 'recipient')}
          />
          <input
            type="text"
            name="address"
            placeholder="Address"
            value={recipient.address}
            onChange={(e) => onChange(e, 'recipient')}
          />
          <input
            type="text"
            name="phone"
            placeholder="Phone"
            value={recipient.phone}
            onChange={(e) => onChange(e, 'recipient')}
          />
        </fieldset>

        <button type="submit" disabled={loading}>
          {loading ? 'Creating...' : 'Create Package'}
        </button>
      </form>
    </div>
  )
}
